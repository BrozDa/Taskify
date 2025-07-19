import axios from "axios";

const baseUrl = "https://localhost:7024/api/Tags";

export const tagsGetAll = async() => {

  try{
    const response = await axios.get(`${baseUrl}/all`,{
      timeout:5000
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