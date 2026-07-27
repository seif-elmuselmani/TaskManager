import api from './api';

export const taskService = {
    getByProject: async (projectId) => {
        const response = await api.get(`/tasks/project/${projectId}`);
        return response.data;
    },
    getByStatus: async (status) => {
        const response = await api.get(`/tasks/status/${status}`);
        return response.data;
    },
    create: async (task) => {
        const response = await api.post('/tasks', task);
        return response.data;
    },
    update: async (id, task) => {
        const response = await api.put(`/tasks/${id}`, task);
        return response.data;
    },
    updateStatus: async (id, status) => {
        const response = await api.patch(`/tasks/${id}/status`, `"${status}"`, {
            headers: { 'Content-Type': 'application/json' }
        });
        return response.data;
    },
    delete: async (id) => {
        const response = await api.delete(`/tasks/${id}`);
        return response.data;
    }
};
