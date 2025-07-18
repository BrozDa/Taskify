
import './App.css'
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import { useAuth } from "./context/AuthContext";
import Login from "./pages/Login";
import Dashboard from "./pages/Dashboard";
import Register from './pages/Register';

function App() {
  const { token } = useAuth();

  const ProtectedRoute = ({ children }) => {
    
    return token ? children : <Navigate to="/login" replace />;
  };

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={
          token ? <Navigate to="/dashboard" /> : <Navigate to="/login" />
        }/>

        <Route path="/login" element={<Login/>}/>
        <Route path="/register" element={<Register/>}/>
        
        <Route path="/dashboard" element={
          <ProtectedRoute>
            <Dashboard />
        </ProtectedRoute>
        }/>

      </Routes>
    </BrowserRouter>
  );

}

export default App
