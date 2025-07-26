import { useState } from "react";
import { DarkModeSwitch } from "react-toggle-dark-mode";
import useDarkMode from "../hooks/useDarkMode"

function DarkModeToggle() {
  const [colorTheme, setTheme] = useDarkMode();
  const [darkMode, setDarkMode] = useState(
    colorTheme === "light" ? true : false
  );

  const toggleDarkMode = (checked) => {
    setTheme(colorTheme);
    setDarkMode(checked);
  };

  return (
    <div className="flex justify-end h-fit m-t">
      <DarkModeSwitch
      checked={darkMode}
      onChange={toggleDarkMode}
      size={30}
    />
    </div>
    
    
  );
}

export default DarkModeToggle