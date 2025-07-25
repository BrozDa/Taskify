import { useNavigate } from 'react-router-dom'
import { useAuth } from "../context/AuthContext";

function LogoutButton() {
  const { setToken } = useAuth();
    const navigate = useNavigate();

    const handleLogout = () => {
        setToken(null);
        navigate("/");
    }

  return (
    <button onClick={() => handleLogout()} className="flex w-full items-center p-2 text-gray-700 hover:bg-gray-200 hover:text-gray-900 rounded-lg">
      <svg xmlns="http://www.w3.org/2000/svg" className="h-6 w-6 mr-2 m" fill="none" viewBox="0 0 24 24" stroke="currentColor">
        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2"
          d="M17 16l4-4m0 0l-4-4m4 4H7m6 4v1a2 2 0 01-2 2H5a2 2 0 01-2-2V7a2 2 0 012-2h6a2 2 0 012 2v1" />
      </svg>
      Log Out
    </button>
  )
}

export default LogoutButton