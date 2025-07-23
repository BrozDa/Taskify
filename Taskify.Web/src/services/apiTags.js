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
  catch(error){
    if(axios.isAxiosError(error)){
      throw new Error(`Something wrong ${error}`);
    }
    throw new Error("Unhandled error - contact administrator")
  }
}
