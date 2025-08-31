import axios from "axios";

const baseUrl = "http://localhost:7024/api/Priorities";

export const prioritiesGetAll = async() => {
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

};