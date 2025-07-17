import React, { useEffect, useState } from 'react'
import LoginInput from '../components/LoginInput';
import Button from '../components/Button';

function Login() {

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [passwordConfirmation, setPasswordConfirmation] = useState("");

  useEffect(() => { }, [password]);

  const handleSubmit = () => {
    console.log(`Username: ${username}`)
    console.log(`Password: ${password}`)
    console.log(`PasswordConfirmation: ${passwordConfirmation}`)
  }

  return (
    <div className="flex items-center justify-center min-h-screen bg-gray-100 dark:bg-gray-900">
      <div className="w-full max-w-md p-8 bg-white rounded-lg shadow-md dark:bg-gray-800">
        <h2 className="mb-6 text-2xl font-semibold text-center text-gray-800 dark:text-white">
          Sign in to your account
        </h2>
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
          <Button 
            type="submit"
            text="Sign In"/>
        </form>
      </div>
    </div>
  );
}

export default Login