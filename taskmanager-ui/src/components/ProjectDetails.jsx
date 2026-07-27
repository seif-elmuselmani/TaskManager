import { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { projectService } from '../services/projectService';
import { taskService } from '../services/taskService';


export default function ProjectDetails() {
    const { id } = useParams();
    const [project, setProject] = useState(null);
    const [tasks, setTasks] = useState([]);
    const [isFormVisible, setFormVisible] = useState(false);
    const [editingTask, setEditingTask] = useState(null);
    
    // Default 1 week from now
    const nextWeek = new Date();
    nextWeek.setDate(nextWeek.getDate() + 7);
    const defaultDate = nextWeek.toISOString().split('T')[0];

    const [formData, setFormData] = useState({ title: '', description: '', dueDate: defaultDate, status: 'ToDo' });

    const loadData = async () => {
        try {
            const proj = await projectService.getById(id);
            setProject(proj);
            const projTasks = await taskService.getByProject(id);
            setTasks(projTasks);
        } catch (error) {
            console.error('Error loading data:', error);
        }
    };

    useEffect(() => {
        loadData();
    }, [id]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            if (editingTask) {
                await taskService.update(editingTask.id, { 
                    id: editingTask.id, 
                    projectId: id, 
                    ...formData,
                    status: formData.status
                });
            } else {
                await taskService.create({ projectId: id, ...formData });
            }
            setFormData({ title: '', description: '', dueDate: defaultDate, status: 'ToDo' });
            setEditingTask(null);
            setFormVisible(false);
            loadData();
        } catch (error) {
            console.error('Error saving task:', error);
        }
    };

    const handleDelete = async (taskId) => {
        if (window.confirm('Are you sure you want to delete this task?')) {
            try {
                await taskService.delete(taskId);
                loadData();
            } catch (error) {
                console.error('Error deleting task:', error);
            }
        }
    };

    const handleStatusChange = async (taskId, newStatus) => {
        try {
            await taskService.updateStatus(taskId, newStatus);
            loadData();
        } catch (error) {
            console.error('Error updating status:', error);
        }
    };

    const startEdit = (task) => {
        setEditingTask(task);
        setFormData({ 
            title: task.title, 
            description: task.description, 
            dueDate: task.dueDate.split('T')[0],
            status: task.status
        });
        setFormVisible(true);
    };

    const getStatusClass = (status) => status === 'ToDo' ? 'todo' : status === 'InProgress' ? 'inprogress' : 'done';

    if (!project) return <div>Loading...</div>;

    return (
        <div>
            <div className="card">
                <h2>{project.name}</h2>
                <p>{project.description}</p>
            </div>

            <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: '20px', alignItems: 'center' }}>
                <h3>Project Tasks</h3>
                <button className="btn btn-primary" onClick={() => {
                    setFormVisible(!isFormVisible);
                    setEditingTask(null);
                    setFormData({ title: '', description: '', dueDate: defaultDate, status: 'ToDo' });
                }}>
                    {isFormVisible ? 'Cancel' : 'Add Task'}
                </button>
            </div>

            {isFormVisible && (
                <div className="card">
                    <h3>{editingTask ? 'Edit Task' : 'New Task'}</h3>
                    <form onSubmit={handleSubmit}>
                        <div className="form-group">
                            <label>Title</label>
                            <input 
                                className="form-control"
                                value={formData.title}
                                onChange={e => setFormData({...formData, title: e.target.value})}
                                required
                            />
                        </div>
                        <div className="form-group">
                            <label>Description</label>
                            <textarea 
                                className="form-control"
                                value={formData.description}
                                onChange={e => setFormData({...formData, description: e.target.value})}
                            />
                        </div>
                        <div className="form-group">
                            <label>Due Date</label>
                            <input 
                                type="date"
                                className="form-control"
                                value={formData.dueDate}
                                onChange={e => setFormData({...formData, dueDate: e.target.value})}
                                required
                            />
                        </div>
                        <button type="submit" className="btn btn-primary">Save Task</button>
                    </form>
                </div>
            )}

            <div className="task-grid">
                {tasks.map(task => (
                    <div key={task.id} className={`task-card ${getStatusClass(task.status)}`}>
                        <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                            <h4>{task.title}</h4>
                            <select
                                className="form-control"
                                style={{ display: 'inline-block', width: 'auto', marginLeft: '10px' }}
                                value={task.status}
                                onChange={e => handleStatusChange(task.id, e.target.value)}
                            >
                                <option value="ToDo">ToDo</option>
                                <option value="InProgress">InProgress</option>
                                <option value="Done">Done</option>
                            </select>
                        </div>
                        <p style={{ marginTop: '10px' }}>{task.description}</p>
                        <small style={{ display: 'block', marginTop: '10px', color: '#666' }}>
                            Due: {new Date(task.dueDate).toLocaleDateString()}
                        </small>
                        <div style={{ marginTop: '15px' }}>
                            <button className="btn" onClick={() => startEdit(task)}>Edit</button>
                            <button className="btn btn-danger" onClick={() => handleDelete(task.id)}>Delete</button>
                        </div>
                    </div>
                ))}
                {tasks.length === 0 && <p>No tasks yet.</p>}
            </div>
        </div>
    );
}
