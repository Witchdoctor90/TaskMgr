import React, { useEffect, useState } from 'react';
import { getAllTasks, createTask, deleteTask } from '../../services/tasksService';
import TaskList from '../../components/tasks/TaskList/TaskList';
import './AllTasks.css';

const AllTasks = () => {
    const [tasks, setTasks] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchTasks = async () => {
            setLoading(true);
            try {
                const taskList = await getAllTasks();
                setTasks(taskList);
            } catch (err) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };

        fetchTasks();
    }, []);

    const handleTaskCreate = async (taskData) => {
        try {
            const newTask = await createTask(taskData);
            setTasks((prevTasks) => [...prevTasks, newTask]);
        } catch (err) {
            setError(err.message);
        }
    };

    const handleTaskDelete = async (taskId) => {
        try {
            await deleteTask(taskId);
            setTasks((prevTasks) => prevTasks.filter((task) => task.id !== taskId));
        } catch (err) {
            setError(err.message);
        }
    };

    if (loading) return <div>Loading tasks...</div>;
    if (error) return <div className="error">{error}</div>;

    return (
        <div className="all-tasks">
            <h1>All Tasks</h1>
            <TaskList tasks={tasks} onDelete={handleTaskDelete} />
            {/* AddTaskForm component should be here to handle task creation */}
        </div>
    );
};

export default AllTasks;