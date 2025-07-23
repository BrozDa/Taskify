import axios from "axios";

const baseUrl = "https://localhost:7024/api/Tasks";

export const tasksGetPending = async() => {
  const token = localStorage.getItem("token");
  if (!token) throw new Error("No token found");
  
  try{
    
    const response = await axios.get(`${baseUrl}/pending`,{
      timeout:5000,
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    console.log(response)
    return response.data;
    
  }
  catch(error){
    if(axios.isAxiosError(error)){
      throw new Error("Something wrong with axios");
    }
    throw new Error("Unhandled error - contact administrator")
  }
}
export const tasksGetCompleted = async() => {
  const token = localStorage.getItem("token");
  if (!token) throw new Error("No token found");
  
  try{
    const response = await axios.get(`${baseUrl}/completed`,{
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
export const tasksCompleteTask = async(taskId) => {
  const token = localStorage.getItem("token");
  if(!token) throw new Error("No token found");

  try{
    const reply = await axios.patch(`${baseUrl}/${taskId}/complete`,
      {
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


export const tasksDeleteTask = async(taskId) => {
  const token = localStorage.getItem("token");
  if(!token) throw new Error("No token found");

  try{
    const reply = await axios.delete(`${baseUrl}/${taskId}`,
      {
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
export const tasksUpdateTags = async(taskId, updatedTags) => {
  const token = localStorage.getItem("token");
  if(!token) throw new Error("No token found");

  try{
    const reply = await axios.patch(`${baseUrl}/${taskId}/tags`,updatedTags,{
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
export const tasksUpdatePriority = async(taskId, updatedPriorityId) => {
  const token = localStorage.getItem("token");
  if(!token) throw new Error("No token found");

  try{
    const reply = await axios.patch(`${baseUrl}/${taskId}/priority`,{updatedPriorityId},{
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
export const tasksUpdateName = async(taskId, newName) => {
  const token = localStorage.getItem("token");
  if(!token) throw new Error("No token found");

  try{
    const reply = await axios.patch(`${baseUrl}/${taskId}/name`,{newName},{
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
export const tasksUpdateDescription = async(taskId, newDescription) => {
  const token = localStorage.getItem("token");
  if(!token) throw new Error("No token found");

  try{
    const reply = await axios.patch(`${baseUrl}/${taskId}/description`,{newDescription},{
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
export const tasksUpdateDate = async(taskId, newDate) => {
  const token = localStorage.getItem("token");
  if(!token) throw new Error("No token found");

  try{
    const reply = await axios.patch(`${baseUrl}/${taskId}/date`,{newDate},{
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