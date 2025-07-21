import axios from "axios";

const baseUrl = "https://localhost:7024/api/Tasks";

export const tasksGetAll = async() => {
  const token = localStorage.getItem("token");
  if (!token) throw new Error("No token found");
  
  try{
    const response = await axios.get(`${baseUrl}/all`,{
      timeout:5000,
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return response.data;
    
  }
  catch(error){
    if(axios.isAxiosError(error)){
      throw new Error("Something wrong with axios");
    }
    throw new Error("Unhandled error - contact administrator")
  }
}
export const tasksAddTask = async(newTask) => {
  const token = localStorage.getItem("token");
  if(!token) throw new Error("No token found");
  try{
    const reply = await axios.post(`${baseUrl}/add`, newTask,{
      timeout: 5000,
      headers :{
        Authorization: `Bearer ${token}`
      },
    })
    return reply.data;
  }
  catch(error){

  }
}
export const tasksDeleteTask = async(taskId) => {
  const token = localStorage.getItem("token");
  if(!token) throw new Error("No token found");

  try{
    const reply = await axios.delete(`${baseUrl}/${taskId}`,{
      timeout: 5000,
      headers :{
        Authorization: `Bearer ${token}`
      },
    })
    return reply.data;
  }
  catch(error){
    if(axios.isAxiosError(error)){
      //do stuff
    }
  }
}