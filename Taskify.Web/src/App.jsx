import { useState } from 'react'
import './App.css'
import { Routes, Route } from "react-router-dom";
import Dashboard from './pages/Dashboard'
import Login from './pages/Login'
import Register from './pages/Register';

function App() {
  const [loggedIn, setLoggedin] = useState(false)

  return (
    <>
    <Routes>
      <Route path="/" element={loggedIn?<Dashboard/> : <Register/>}/>
    </Routes>
    </>
  )
}

export default App
