import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { projectService } from '../services/projectService';

export default function ProjectsList() {
    const [projects, setProjects] = useState([]);
    const [isFormVisible, setFormVisible] = useState(false);
    const [editingProject, setEditingProject] = useState(null);
    const [formData, setFormData] = useState({ name: '', description: '' });

    const fetchProjects = async () => {
        try {
            const data = await projectService.getAll();
            setProjects(data);
        } catch (error) {
            console.error('Error fetching projects:', error);
        }
    };

    useEffect(() => {
        fetchProjects();
    }, []);

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            if (editingProject) {
                await projectService.update(editingProject.id, { id: editingProject.id, ...formData });
            } else {
                await projectService.create(formData);
            }
            setFormData({ name: '', description: '' });
            setEditingProject(null);
            setFormVisible(false);
            fetchProjects();
        } catch (error) {
            console.error('Error saving project:', error);
        }
    };

    const handleDelete = async (id) => {
        if (window.confirm('Are you sure you want to delete this project?')) {
            try {
                await projectService.delete(id);
                fetchProjects();
            } catch (error) {
                console.error('Error deleting project:', error);
            }
        }
    };

    const startEdit = (project) => {
        setEditingProject(project);
        setFormData({ name: project.name, description: project.description });
        setFormVisible(true);
    };

    return (
        <div>
            <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: '20px' }}>
                <h2>Projects</h2>
                <button className="btn btn-primary" onClick={() => {
                    setFormVisible(!isFormVisible);
                    setEditingProject(null);
                    setFormData({ name: '', description: '' });
                }}>
                    {isFormVisible ? 'Cancel' : 'Add Project'}
                </button>
            </div>

            {isFormVisible && (
                <div className="card">
                    <h3>{editingProject ? 'Edit Project' : 'New Project'}</h3>
                    <form onSubmit={handleSubmit}>
                        <div className="form-group">
                            <label>Name</label>
                            <input 
                                className="form-control"
                                value={formData.name}
                                onChange={e => setFormData({...formData, name: e.target.value})}
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
                        <button type="submit" className="btn btn-primary">Save</button>
                    </form>
                </div>
            )}

            <div className="task-grid">
                {projects.map(project => (
                    <div key={project.id} className="card">
                        <h3>{project.name}</h3>
                        <p>{project.description}</p>
                        <small>Created: {new Date(project.createdAt).toLocaleDateString()}</small>
                        <div style={{ marginTop: '15px' }}>
                            <Link to={`/project/${project.id}`} className="btn btn-primary" style={{ display: 'inline-block', textAlign: 'center' }}>View Tasks</Link>
                            <button className="btn" onClick={() => startEdit(project)}>Edit</button>
                            <button className="btn btn-danger" onClick={() => handleDelete(project.id)}>Delete</button>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
}
