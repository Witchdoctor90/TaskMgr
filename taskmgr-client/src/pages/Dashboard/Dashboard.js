import React, { useEffect, useState } from "react";
import LoggedInHeader from "../../components/LoggedInHeader";
import { getAllTasks, updateTask, createTask } from "../../services/tasksService";
import TaskDetails from "../../components/modal/TaskDetails";

export default function Dashboard() {
  const [tasks, setTasks] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [newTaskTitle, setNewTaskTitle] = useState('');
  const [creating, setCreating] = useState(false);
  const [modalTask, setModalTask] = useState(null);
  const [search, setSearch] = useState('');

  const fetchTasks = async () => {
    setLoading(true);
    setError("");
    try {
      const data = await getAllTasks();
      setTasks(Array.isArray(data) ? data : []);
    } catch (err) {
      setError("Failed to load tasks");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchTasks();
  }, []);

  const handleToggleDone = async (task) => {
    try {
      const newStatus = task.status === 1 ? 0 : 1;
      await updateTask({ ...task, status: newStatus });
      fetchTasks();
    } catch {
      alert("Failed to update task");
    }
  };

  const handleCreateTask = async (e) => {
    e.preventDefault();
    if (!newTaskTitle.trim()) return;
    setCreating(true);
    try {
      await createTask({ title: newTaskTitle.trim() });
      setNewTaskTitle('');
      fetchTasks();
    } catch {
      alert("Failed to create task");
    } finally {
      setCreating(false);
    }
  };

  // SVG синій кружечок всередині сірого контуру (outline)
  const CircleButton = ({ done, onClick }) => (
    <button
      onClick={onClick}
      aria-label={done ? "Unmark as done" : "Mark as done"}
      style={{
        width: 28,
        height: 28,
        border: "2px solid #bbb",
        borderRadius: "50%",
        background: "#fff",
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        cursor: "pointer",
        marginLeft: 16,
        outline: "none",
        position: "relative",
        padding: 0,
      }}
      type="button"
      tabIndex={-1}
    >
      {done && (
        <span
          style={{
            display: "block",
            width: 16,
            height: 16,
            borderRadius: "50%",
            background: "#2563eb",
            transition: "background 0.2s",
          }}
        />
      )}
    </button>
  );

  // Функція для "розумного" пошуку по назві таски
  function matchesSearch(title, search) {
    if (!search) return true;
    if (!title) return false;
    const t = title.toLowerCase();
    const s = search.toLowerCase();
    if (t.includes(s)) return true;
    for (let len = Math.min(s.length, t.length); len >= 3; len--) {
      for (let i = 0; i <= s.length - len; i++) {
        const sub = s.slice(i, i + len);
        if (t.includes(sub)) return true;
      }
    }
    return false;
  }

  // Для дропдауна: максимум 8 тасок, тільки якщо є пошук
  const searchResults = search
    ? tasks.filter(task => matchesSearch(task.title, search)).slice(0, 8)
    : [];

  return (
    <div>
      <LoggedInHeader
        spinning={loading}
        onSearch={setSearch}
        searchResults={searchResults}
        onSelectTask={task => setModalTask(task)}
      />
      <TaskDetails
        open={!!modalTask}
        onClose={(action) => {
          setModalTask(null);
          if (action === "updated" || action === "deleted") {
            fetchTasks();
          }
        }}
        task={modalTask}
      />
      <div
        style={{
          display: "flex",
          flexDirection: "row",
          minHeight: "80vh",
          background: "#f6f7fb",
        }}
      >
        {/* Tasks column (fixed width, left) */}
        <div
          style={{
            width: "25vw",
            minWidth: 320,
            maxWidth: 420,
            background: "#fff",
            borderRadius: "16px",
            margin: "32px 24px 32px 32px",
            padding: "24px 18px",
            boxShadow: "0 2px 12px rgba(0,0,0,0.04)",
            display: "flex",
            flexDirection: "column",
            alignItems: "stretch",
            height: "fit-content",
          }}
        >
          <h2 style={{ marginBottom: 18 }}>Tasks</h2>
          {/* {loading && <div>Loading...</div>} */}
          {error && <div style={{ color: "red" }}>{error}</div>}

          {/* Інлайн-редактор для створення таски */}
          <form
            onSubmit={handleCreateTask}
            style={{
              display: "flex",
              alignItems: "center",
              gap: 0,
              marginBottom: tasks.length > 0 ? 18 : 0,
              marginTop: tasks.length === 0 ? 24 : 0,
              background: "#f3f4f6",
              borderRadius: 8,
              padding: "2px 6px 2px 12px",
              border: "1px solid #f3f4f6",
              boxShadow: "none",
            }}
          >
            <input
              type="text"
              className="auth-input"
              placeholder="Enter new task title"
              value={newTaskTitle}
              onChange={e => setNewTaskTitle(e.target.value)}
              style={{
                flex: 1,
                border: "none",
                outline: "none",
                background: "transparent",
                fontSize: 16,
                padding: "10px 0",
                boxShadow: "none",
              }}
              disabled={creating}
            />
            <button
              type="submit"
              style={{
                background: "none",
                border: "none",
                color: "#2563eb",
                fontSize: 22,
                cursor: creating || !newTaskTitle.trim() ? "not-allowed" : "pointer",
                padding: "0 8px",
                display: "flex",
                alignItems: "center",
                opacity: creating || !newTaskTitle.trim() ? 0.5 : 1,
                transition: "opacity 0.2s",
              }}
              disabled={creating || !newTaskTitle.trim()}
              aria-label="Add task"
              title="Add task"
            >
              +
            </button>
          </form>

          {/* Якщо таски є — показати список */}
          {tasks.length > 0 && (
            <div style={{ display: "flex", flexDirection: "column", gap: 12 }}>
              {tasks.map((task) => (
                <div
                  key={task.id}
                  style={{
                    display: "flex",
                    alignItems: "center",
                    justifyContent: "space-between",
                    padding: "14px 18px",
                    border: "1px solid #eee",
                    borderRadius: 8,
                    background: "#fafbfc",
                    cursor: "pointer"
                  }}
                  onClick={() => setModalTask(task)}
                >
                  <span>{task.title || "Untitled"}</span>
                  <CircleButton
                    done={task.status === 1}
                    onClick={e => {
                      e.stopPropagation();
                      handleToggleDone(task);
                    }}
                  />
                </div>
              ))}
            </div>
          )}
        </div>
        {/* Місце для майбутніх колонок */}
        <div style={{ flex: 1 }}></div>
      </div>
    </div>
  );
}

