
import './App.css'
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import { useAuth } from "./context/AuthContext";
import Login from "./pages/Login";
import Dashboard from "./pages/Dashboard";
import Register from './pages/Register';
import CompletedTasks from "./pages/CompletedTasks"
import DashboardLayout from './layouts/DashboardLayout';

import ReactModal from 'react-modal';

ReactModal.setAppElement('#root');

function App() {
  const { token } = useAuth();

  const ProtectedRoute = ({ children }) => {

    return token ? children : <Navigate to="/login" replace />;
  };

  return (
    <>
      <BrowserRouter>
      <Routes>
        {/* Redirect root based on auth */}
        <Route path="/" element={
          token ? <Navigate to="/dashboard" /> : <Navigate to="/login" />
        } />

        {/* Public routes */}
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />

        {/* Protected routes */}
        <Route path="/dashboard" element={
          <ProtectedRoute>
            <DashboardLayout />
          </ProtectedRoute>
        }>
          <Route index element={<Dashboard />} />
          <Route path="completed" element={<CompletedTasks />} />
        </Route>

      </Routes>
    </BrowserRouter>
    </>
  );

}

export default App
