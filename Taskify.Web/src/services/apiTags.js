import axios from "axios";

const baseUrl = "https://localhost:7024/api/Tags";

export const tagsGetAll = async() => {

  const token = localStorage.getItem("token");
  if (!token) throw new Error("No token found");

  try{
    const response = await axios.get(`${baseUrl}`,{
      timeout:5000,
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return response.data;
    
  }
  catch (error) {
    if (axios.isAxiosError(error)) {
      const message = error.response?.data || error.message;

      throw new Error(`Request failed: ${message}`);
    }
    throw new Error("Unhandled error - contact administrator")
  }
}

export const tagsAdd = async(newTag) => {

  const token = localStorage.getItem("token");
  if (!token) throw new Error("No token found");

  try{
    console.log(newTag)
    const response = await axios.post(`${baseUrl}`, newTag, {
      timeout: 5000,
      headers: {
        Authorization: `Bearer ${token}`
      },
    })
    return response.data;
  }
  catch (error) {
    if (axios.isAxiosError(error)) {
      const message = error.response?.data || error.message;

      throw new Error(`Request failed: ${message}`);
    }
    throw new Error("Unhandled error - contact administrator")
  }
}
export const tagsAddForNewTask = async(name) => {

  const token = localStorage.getItem("token");
  if (!token) throw new Error("No token found");

  try{
    const response = await axios.post(`${baseUrl}/new/${name}`, {
      timeout: 5000,
      headers: {
        Authorization: `Bearer ${token}`
      },
    })
    return response.data;
  }
  catch (error) {
    if (axios.isAxiosError(error)) {
      const message = error.response?.data || error.message;

      throw new Error(`Request failed: ${message}`);
    }
    throw new Error("Unhandled error - contact administrator")
  }
}

