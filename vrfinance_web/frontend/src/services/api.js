import axios from "axios";

const api = axios.create({
    baseURL: "/api",
});

export async function getTransactions() {
    try {
        const response = await api.get("/transactions");
        return response.data;
    } catch (error) {
        console.error("Failed to fetch transactions:", error);
        throw error;
    }
}

export async function getStatements() {
    try {
        const response = await api.get("/statements");
        return response.data;
    } catch (error) {
        console.error("Failed to fetch statements:", error);
        throw error;
    }
}

export async function getStatementTransactions(id) {
    try {
        const response = await api.get(`/statements/${id}/transactions`);
        return response.data;
    } catch (error) {
        console.error(`Failed to fetch transactions for statement ${id}:`, error);
        throw error;
    }
}

export async function deleteStatement(id) {
    try {
        const response = await api.delete(`/statements/${id}`);
        return response.data;
    } catch (error) {
        console.error(`Failed to delete statement ${id}:`, error);
        throw error;
    }
}

export async function uploadStatement(file, name) {
    try {
        const formData = new FormData();
        formData.append("file", file);
        formData.append("name", name);

        const response = await api.post("/statements/upload", formData, {
            headers: { "Content-Type": "multipart/form-data" },
        });

        return response.data;
    } catch (error) {
        console.error("Failed to upload statement:", error);
        throw error;
    }
}

export async function getStatementCategories(id) {
    try {
        const response = await api.get(`/statements/${id}/categories`);
        return response.data;
    } catch (error) {
        console.error(`Failed to fetch categories for statement ${id}:`, error);
        throw error;
    }
}

export default api;
