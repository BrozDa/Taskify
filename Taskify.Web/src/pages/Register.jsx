import { useEffect, useState } from 'react'
import LoginInput from '../components/LoginInput';
import Button from '../components/Button';
import {userRegistration} from "../services/api";
import { useNavigate } from "react-router-dom";

function Register() {

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [passwordConfirmation, setPasswordConfirmation] = useState("");
  const [error, setError] = useState(null);

  const navigate = useNavigate();

  const handleSubmit = async(username, password, e) => {

      e.preventDefault();

      if(password !== passwordConfirmation){
        setError("Passwords do not match");
        return;
      }

      try {
        await userRegistration(username, password);
        navigate("/login", { state: { successMessage: "Account created!" } });

      } catch (err) {
        setError(err);
      }
    }

  return (
    <div className="flex items-center justify-center min-h-screen bg-gray-100 dark:bg-gray-900">
      <div className="w-full max-w-md p-8 bg-white rounded-lg shadow-md dark:bg-gray-800">
        <h2 className="mb-6 text-2xl font-semibold text-center text-gray-800 dark:text-white">
          Create new account
        </h2>
        {error && <p className="text-red-500 mb-3">{error}</p>}
        <form onSubmit={handleSubmit} className="space-y-4">
          <LoginInput
            label="Username"
            id="username"
            type="text"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            required
          />
          <LoginInput
            label="Password"
            id="password"
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
          <LoginInput
            label="Confirm password"
            id="passwordConfirmation"
            type="password"
            value={passwordConfirmation}
            onChange={(e) => setPasswordConfirmation(e.target.value)}
            required
          />
          <Button 
            type="submit"
            text="Create account"
            action={(e) => handleSubmit(username, password,e)}/>
        </form>
      </div>
    </div>
  );
}

export default Register