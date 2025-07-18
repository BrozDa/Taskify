import axios from "axios";

import {
  createContext,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";


const AuthContext = createContext();

const AuthContextProvider = ({children}) => {
  const [token, setToken] = useState(localStorage.getItem("token"));

  const contextValue = useMemo(
    () => ({
      token,
      setToken,
    }),
    [token]
  );

  useEffect(() => {
    if (token) {
      axios.defaults.headers.common["Authorization"] = "Bearer " + token;
      localStorage.setItem('token',token);
    } else {
      delete axios.defaults.headers.common["Authorization"];
      localStorage.removeItem('token')
    }
  }, [token]);

  return (
  <AuthContext.Provider value={contextValue}>
    {children}
  </AuthContext.Provider>
  );
}

export const useAuth = () => {
  return useContext(AuthContext);
};

export default AuthContextProvider;
