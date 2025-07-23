import React from 'react'
import { useState, useEffect } from 'react';
function TaskTag({ tags, setNewTag }) {

  const [dropDownOpen, setDropDownOpen] = useState(false);
  const [isButtonActive, setButtonActive] = useState(false);
  
  const handleMouseEnter = () => {
    setDropDownOpen(true);
    setButtonActive(true);
  }
  const handleMouseLeave = () => {
    setDropDownOpen(false);
    setButtonActive(false);
  }
  useEffect(() => {}, [tags]);  

  return (
    <div
      onMouseEnter={handleMouseEnter}
      onMouseLeave={handleMouseLeave}
      className="relative inline-block"
    >
      <span className={`w-5 h-5 m-0 p-0 rounded-full flex items-center justify-center
                      ${isButtonActive ? "bg-black/60" : "bg-black/30"} hover:border-none  text-white focus:outline-none active:scale-110 transition-transform duration-150`}
      >
        <svg xmlns="http://www.w3.org/2000/svg" className="h-4 w-4" fill="none" viewBox="0 0 24 24"
          stroke="currentColor">
          <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="3" d="M12 6v12M6 12h12" />
        </svg>
      </span>
      {dropDownOpen && 
        <div id="dropdown" className="absolute -left-10 z-30 bg-white divide-y divide-gray-100 rounded-lg shadow-sm w-24">
          <ul className="py-0.5 text-sm text-gray-700 dark:text-gray-200" aria-labelledby="dropdownDefaultButton">
            {tags.map(t => (
              <li key={t.id}>
                <span className="block px-2 py-1 hover:bg-gray-100 hover:rounded-lg cursor-pointer" onClick={() => setNewTag(t)}>{t.name}</span>
              </li>
            ))}
          </ul>
        </div>
      }
    </div>
  )

}

export default TaskTag