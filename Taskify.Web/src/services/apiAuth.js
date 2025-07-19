import axios from "axios";

const baseUrl = "https://localhost:7024/api/Auth";

export const authLogin = async (username, password) => {

  try{
    const response = await axios
    .post(`${baseUrl}/login`, {
      username,
      password,
      }, 
      {
        headers: {
          'Accept': 'text/plain',
          'Content-Type': 'application/json'
        },
        timeout: 5000, 
      });

    if (!response.data) {
      throw new Error("Empty response from server.");
    }
    return response.data
  }
  catch (error){
    if(axios.isAxiosError(error)){
      const status = error.response?.status;
      const message = error.response?.data || error.message;

      if(status === 401){
        throw new Error("Incorrect username or password");
      }
      if(!error.response){
        throw new Error("Server not responding");
      }
      throw new Error(`Request failed: ${message}`);
    }
    throw new Error("Unhandled error - contact administrator")
  }
  
};
export const authRegistration = async(username, password) => {
  try{
    const response = await axios
    .post(`${baseUrl}/register`, {
      username,
      password,
      "role" : "user"
      }, 
      {
        headers: {
          'Accept': 'text/plain',
          'Content-Type': 'application/json'
        },
        timeout: 5000, 
      });

    if (!response.data) {
      throw new Error("Empty response from server.");
    }
    return response.data
  }
  catch (error){
    if(axios.isAxiosError(error)){
      const status = error.response?.status;
      const message = error.response?.data || error.message;

      if(status === 401){
        throw new Error("User with this username already exists")
      }
      if(!error.response){
        throw new Error("Server not responding");
      }
      throw new Error(`Request failed: ${message}`);
    }
    throw new Error("Unhandled server error");
  }
}