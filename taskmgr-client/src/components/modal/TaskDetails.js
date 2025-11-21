import React, { useEffect, useState } from "react";
import { getUserInfo } from "../../services/authService";
import { updateTask, deleteTask } from "../../services/tasksService";

// TaskStatus enum mapping (based on openapi.json)
const TASK_STATUS = {
  0: "Not Started",
  1: "Done",
  2: "In Progress",
  3: "Postponed",
  4: "Cancelled",
  5: "Failed",
  6: "Deleted",
  7: "Archived",
};

const TASK_STATUS_OPTIONS = Object.entries(TASK_STATUS).map(([value, label]) => ({
  value: Number(value),
  label,
}));

export default function TaskDetails({ open, onClose, task }) {
  const [username, setUsername] = useState("—");
  const [editMode, setEditMode] = useState(false);
  const [editTitle, setEditTitle] = useState("");
  const [editContent, setEditContent] = useState("");
  const [editDeadline, setEditDeadline] = useState("");
  const [saving, setSaving] = useState(false);
  const [deleting, setDeleting] = useState(false);
  const [status, setStatus] = useState(task?.status ?? 0);

  useEffect(() => {
    let ignore = false;
    if (open) {
      getUserInfo()
        .then((user) => {
          if (!ignore && user && user.username) setUsername(user.username);
        })
        .catch(() => {
          if (!ignore) setUsername("—");
        });
    }
    return () => { ignore = true; };
  }, [open]);

  useEffect(() => {
    if (open && task) {
      setEditMode(false);
      setEditTitle(task.title || "");
      setEditContent(task.content || "");
      setEditDeadline(task.deadline ? new Date(task.deadline).toISOString().slice(0, 16) : "");
      setStatus(typeof task.status === "number" ? task.status : 0);
    }
  }, [open, task]);

  if (!open || !task) return null;

  const createdDate = task.createdAt ? new Date(task.createdAt) : null;
  const deadlineDate = task.deadline ? new Date(task.deadline) : null;

  const handleSave = async () => {
    setSaving(true);
    try {
      await updateTask({
        ...task,
        title: editTitle,
        content: editContent,
        deadline: editDeadline ? new Date(editDeadline).toISOString() : null,
        status: status,
      });
      setEditMode(false);
      if (typeof onClose === "function") onClose("updated");
    } catch {
      // handle error if needed
    } finally {
      setSaving(false);
    }
  };

  const handleDelete = async () => {
    if (!window.confirm("Are you sure you want to delete this task?")) return;
    setDeleting(true);
    try {
      await deleteTask(task.id);
      if (typeof onClose === "function") onClose("deleted");
    } catch {
      // handle error if needed
    } finally {
      setDeleting(false);
    }
  };

  // Зберігати статус одразу при виборі (без кнопки Save)
  const handleStatusChange = async (e) => {
    const newStatus = Number(e.target.value);
    setStatus(newStatus);
    try {
      await updateTask({
        ...task,
        status: newStatus,
      });
      if (typeof onClose === "function") onClose("updated");
    } catch {
      // handle error if needed
    }
  };

  return (
    <>
      {/* Overlay */}
      <div
        onClick={onClose}
        style={{
          position: "fixed",
          top: 0,
          left: 0,
          width: "100vw",
          height: "100vh",
          background: "rgba(30, 41, 59, 0.32)",
          zIndex: 999,
          transition: "opacity 0.25s",
          opacity: open ? 1 : 0,
          pointerEvents: open ? "auto" : "none",
        }}
      />
      {/* Modal */}
      <div
        style={{
          position: "fixed",
          top: 0,
          right: 0,
          width: "75vw",
          height: "100vh",
          background: "#fff",
          boxShadow: "-4px 0 32px rgba(0,0,0,0.08)",
          zIndex: 1000,
          display: "flex",
          flexDirection: "column",
          overflowY: "auto",
          transform: open ? "translateX(0)" : "translateX(100%)",
          opacity: open ? 1 : 0,
          transition: "transform 0.25s cubic-bezier(.4,1,.4,1), opacity 0.25s cubic-bezier(.4,1,.4,1)",
        }}
      >
        <div
          style={{
            padding: "24px 32px 16px 32px",
            borderBottom: "1px solid #eee",
            display: "flex",
            alignItems: "center",
            justifyContent: "space-between",
          }}
        >
          {/* Title and Close Button */}
          {editMode ? (
            <input
              value={editTitle}
              onChange={e => setEditTitle(e.target.value)}
              style={{
                fontSize: 24,
                fontWeight: 700,
                border: "1px solid #e5e7eb",
                borderRadius: 6,
                padding: "6px 12px",
                width: 400,
                maxWidth: "60vw",
                minWidth: 120,
                margin: 0,
                background: "#f6f7fa",
              }}
              disabled={saving}
            />
          ) : (
            <div
              style={{
                fontSize: 24,
                fontWeight: 700,
                minWidth: 120,
                maxWidth: 400,
                overflow: "hidden",
                textOverflow: "ellipsis",
                whiteSpace: "nowrap",
                borderRadius: 6,
                padding: "6px 12px",
                margin: 0,
                boxSizing: "border-box",
              }}
            >
              {task.title || "Untitled task"}
            </div>
          )}
          <button
            onClick={onClose}
            style={{
              fontSize: 24,
              background: "none",
              border: "none",
              cursor: "pointer",
              color: "#888",
              marginLeft: 16,
            }}
            aria-label="Close"
          >
            &times;
          </button>
        </div>
        <div
          style={{
            display: "flex",
            flex: 1,
            padding: "32px",
            gap: 32,
            alignItems: "flex-start",
          }}
        >
          {/* LEFT: статус (dropdown) + опис + кнопки */}
          <div style={{ flex: 2, minWidth: 0 }}>
            {/* Статус як синій клікабельний дропдаун без бордера і бекграунда */}
            <div style={{ marginBottom: 18 }}>
              <select
                value={status}
                onChange={handleStatusChange}
                style={{
                  fontWeight: 700,
                  fontSize: 18,
                  color: "#2563eb",
                  letterSpacing: 1,
                  textTransform: "uppercase",
                  textAlign: "center",
                  background: "none",
                  border: "none",
                  outline: "none",
                  cursor: "pointer",
                  padding: "6px 0",
                  minWidth: 160,
                  borderRadius: 10,
                  appearance: "none",
                  WebkitAppearance: "none",
                  MozAppearance: "none",
                  textAlignLast: "center", // для центрування тексту в дропдауні
                  boxShadow: "0 1px 4px rgba(37,99,235,0.07)",
                  transition: "box-shadow 0.2s",
                }}
                disabled={saving || deleting}
              >
                {TASK_STATUS_OPTIONS.map(opt => (
                  <option
                    key={opt.value}
                    value={opt.value}
                    style={{
                      color: "#222",
                      background: "#fff",
                      textAlign: "center",
                    }}
                  >
                    {opt.label}
                  </option>
                ))}
              </select>
            </div>
            {/* Опис */}
            {editMode ? (
              <textarea
                value={editContent}
                onChange={e => setEditContent(e.target.value)}
                style={{
                  background: "#f6f7fa",
                  borderRadius: 12,
                  minHeight: 180,
                  maxHeight: 260,
                  height: 180,
                  width: "100%",
                  padding: "24px 20px",
                  fontSize: "1.15rem",
                  color: "#222",
                  boxSizing: "border-box",
                  marginBottom: 12,
                  border: "1px solid #e5e7eb",
                  fontWeight: 500,
                  resize: "none",
                  textAlign: "left",
                  overflowY: "auto",
                }}
                disabled={saving}
              />
            ) : (
              <div
                style={{
                  background: "#f6f7fa",
                  borderRadius: 12,
                  minHeight: 180,
                  maxHeight: 260,
                  padding: "24px 20px",
                  fontSize: "1.15rem",
                  color: "#222",
                  overflowY: "auto",
                  boxSizing: "border-box",
                  marginBottom: 12,
                  border: "1px solid #e5e7eb",
                  fontWeight: 500,
                  textAlign: "left"
                }}
              >
                {task.content
                  ? task.content
                  : <span style={{ color: "#bbb", textAlign: "left", display: "block" }}>No description</span>}
              </div>
            )}
            {/* Кнопки під описом */}
            <div
              style={{
                marginTop: 24,
                display: "flex",
                gap: 10,
                flexWrap: "wrap",
                alignItems: "center",
                minHeight: 40,
              }}
            >
              <button
                onClick={
                  editMode
                    ? handleSave
                    : () => setEditMode(true)
                }
                style={{
                  background: "#f3f4f6",
                  border: "1px solid #e5e7eb",
                  color: "#2563eb",
                  borderRadius: 6,
                  padding: "6px 18px",
                  fontWeight: 600,
                  cursor: "pointer",
                  fontSize: 15,
                }}
                disabled={editMode ? saving : false}
              >
                {editMode ? (saving ? "Saving..." : "Save") : "Edit task"}
              </button>
              <button
                onClick={
                  editMode
                    ? () => setEditMode(false)
                    : handleDelete
                }
                style={{
                  background: "#fff0f0",
                  border: "1px solid #fca5a5",
                  color: "#dc2626",
                  borderRadius: 6,
                  padding: "6px 18px",
                  fontWeight: 600,
                  cursor: "pointer",
                  fontSize: 15,
                }}
                disabled={editMode ? saving : deleting}
              >
                {editMode ? "Cancel" : (deleting ? "Deleting..." : "Delete task")}
              </button>
            </div>
          </div>
          {/* RIGHT: user + дати + дедлайн */}
          <div style={{
            flex: 1,
            minWidth: 220,
            maxWidth: 320,
            display: "flex",
            flexDirection: "column",
            alignItems: "flex-start",
            gap: 18,
            background: "#f9fafb",
            borderRadius: 12,
            padding: "24px 20px",
            border: "1px solid #f1f1f1"
          }}>
            <div style={{ fontWeight: 600, fontSize: 18, color: "#222", marginBottom: 8 }}>
              {username}
            </div>
            <div style={{ fontSize: 15, color: "#555", marginBottom: 6 }}>
              <div style={{ fontWeight: 500, color: "#888" }}>Created</div>
              <div style={{
                fontSize: 15,
                color: "#555",
                background: "#f6f7fa",
                borderRadius: 6,
                padding: "9px 62px",
                boxSizing: "border-box",
                minHeight: 38,
                display: "flex",
                alignItems: "center"
              }}>
                {createdDate
                  ? createdDate.toLocaleDateString() + " " + createdDate.toLocaleTimeString([], { hour: "2-digit", minute: "2-digit" })
                  : "—"}
              </div>
            </div>
            <div style={{ fontSize: 15, color: "#555" }}>
              <div style={{ fontWeight: 500, color: "#888" }}>Deadline</div>
              {editMode ? (
                <input
                  type="datetime-local"
                  value={editDeadline}
                  onChange={e => setEditDeadline(e.target.value)}
                  style={{
                    padding: "8px 12px",
                    borderRadius: 6,
                    border: "1px solid #e5e7eb",
                    fontSize: 15,
                    width: 240,
                    maxWidth: "100%",
                    background: "#f6f7fa",
                  }}
                  disabled={saving}
                />
              ) : (
                <div
                  style={{
                    fontSize: 15,
                    color: "#555",
                    background: "#f6f7fa",
                    borderRadius: 6,
                    padding: "9px 62px",
                    boxSizing: "border-box",
                    minHeight: 38,
                    display: "flex",
                    alignItems: "center"
                  }}
                >
                  {deadlineDate
                    ? deadlineDate.toLocaleDateString() + " " + deadlineDate.toLocaleTimeString([], { hour: "2-digit", minute: "2-digit" })
                    : "—"}
                </div>
              )}
            </div>
          </div>
        </div>
      </div>
    </>
  );
}
