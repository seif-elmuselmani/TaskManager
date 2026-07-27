import api from './api';

export const projectService = {
    getAll: async () => {
        const response = await api.get('/projects');
        return response.data;
    },
    getById: async (id) => {
        const response = await api.get(`/projects/${id}`);
        return response.data;
    },
    create: async (project) => {
        const response = await api.post('/projects', project);
        return response.data;
    },
    update: async (id, project) => {
        const response = await api.put(`/projects/${id}`, project);
        return response.data;
    },
    delete: async (id) => {
        const response = await api.delete(`/projects/${id}`);
        return response.data;
    }
};
