import { jwtDecode } from 'jwt-decode'
import { useState, useEffect } from 'react';



function DashboardHeader() {
  const [username, setUsername] = useState(null);

   useEffect(() => {
    const token = localStorage.getItem('token');
    if (token) {
      try {
        const decoded = jwtDecode(token);
        setUsername(decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]);
      } catch (err) {
        console.error("Invalid token", err);
      }
    }
  }, []);


  return (
    <div className="flex justify-center items-center h-16 bg-white border-b border-gray-200">
            <span className="text-black text-2xl font-bold uppercase ">
              {`Welcome ${username ? username : "Unknown"}`}
            </span>
     </div>
  )
}

export default DashboardHeader