import axios from "axios";

const baseUrl = "http://localhost:7024/api/Tasks";


const handleErrorResponse = (error) => {
  if (axios.isAxiosError(error)) {
    const status = error.response?.status;
    const message = error.response?.data || error.message;

    switch(status){
      case 400: return `Bad request: ${message}`;
      case 401: return "Unauthorized Acccess";
      case 404: return "Not Found"
      default: return `Request failed: ${message}`;
    }
  }
  return "Unhandled Error - Contact Administrator";
}
export const tasksGetPending = async () => {
  const token = localStorage.getItem("token");
  if (!token) throw new Error("User not logged in");
  try {
    const response = await axios.get(`${baseUrl}/pending`, {
      timeout: 5000,
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return response.data;
  }
  catch (error) {
    throw new Error(handleErrorResponse(error));
  }
}
export const tasksGetCompleted = async () => {
  const token = localStorage.getItem("token");
  if (!token) throw new Error("User not logged in");

  try {
    const response = await axios.get(`${baseUrl}/completed`, {
      timeout: 5000,
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return response.data;

  }
  catch (error) {
    throw new Error(handleErrorResponse(error));
  }
}
export const tasksAddTask = async (newTask) => {
  const token = localStorage.getItem("token");
  if (!token) throw new Error("User not logged in");
  try {
    const response = await axios.post(`${baseUrl}/add`, newTask, {
      timeout: 5000,
      headers: {
        Authorization: `Bearer ${token}`
      },
    })
    return response.data;
  }
  catch (error) {
    throw new Error(handleErrorResponse(error));
  }
}
export const tasksCompleteTask = async (taskId) => {
  const token = localStorage.getItem("token");
  if (!token) throw new Error("No token found");

  try {
    const response = await axios.patch(`${baseUrl}/${taskId}/complete`,
      {
        timeout: 5000,
        headers: {
          Authorization: `Bearer ${token}`
        },
      })
    return response.data;
  }
  catch (error) {
    throw new Error(handleErrorResponse(error));
  }
}
export const tasksDeleteTask = async (taskId) => {
  const token = localStorage.getItem("token");
  if (!token) throw new Error("No token found");

  try {
    const response = await axios.delete(`${baseUrl}/${taskId}`,
      {
        timeout: 5000,
        headers: {
          Authorization: `Bearer ${token}`
        },
      })
    return response.data;
  }
  catch (error) {
    throw new Error(handleErrorResponse(error));
  }
}
export const tasksUpdateTags = async (taskId, updatedTags) => {
  const token = localStorage.getItem("token");
  if (!token) throw new Error("User not logged in");

  try {
    const response = await axios.patch(`${baseUrl}/${taskId}/tags`, updatedTags, {
      timeout: 5000,
      headers: {
        Authorization: `Bearer ${token}`
      },
    })
    return response.data;
  }
  catch (error) {
    throw new Error(handleErrorResponse(error));
  }

}
export const tasksUpdatePriority = async (taskId, updatedPriorityId) => {
  const token = localStorage.getItem("token");
  if (!token) throw new Error("User not logged in");

  try {
    const response = await axios.patch(`${baseUrl}/${taskId}/priority`, { updatedPriorityId }, {
      timeout: 5000,
      headers: {
        Authorization: `Bearer ${token}`
      },
    })
    return response.data;
  }
  catch (error) {
    throw new Error(handleErrorResponse(error));
  }
}
export const tasksUpdateName = async (taskId, newName) => {
  const token = localStorage.getItem("token");
  if (!token) throw new Error("User not logged in");

  try {
    const response = await axios.patch(`${baseUrl}/${taskId}/name`, { newName }, {
      timeout: 5000,
      headers: {
        Authorization: `Bearer ${token}`
      },
    })
    return response.data;
  }
  catch (error) {
    throw new Error(handleErrorResponse(error));
  }
}
export const tasksUpdateDescription = async (taskId, newDescription) => {
  const token = localStorage.getItem("token");
  if (!token) throw new Error("User not logged in");

  try {
    const response = await axios.patch(`${baseUrl}/${taskId}/description`, { newDescription }, {
      timeout: 5000,
      headers: {
        Authorization: `Bearer ${token}`
      },
    })
    return response.data;
  }
  catch (error) {
    throw new Error(handleErrorResponse(error));
  }
}
export const tasksUpdateDate = async (taskId, newDate) => {
  const token = localStorage.getItem("token");
  if (!token) throw new Error("User not logged in");

  try {
    const response = await axios.patch(`${baseUrl}/${taskId}/due-date`, { newDate }, {
      timeout: 5000,
      headers: {
        Authorization: `Bearer ${token}`
      },
    })
    return response.data;
  }
  catch (error) {
    throw new Error(handleErrorResponse(error));
  }
}