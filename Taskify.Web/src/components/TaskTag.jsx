import React from 'react'
import { useState, useEffect, useRef } from 'react';
import ReactModal from 'react-modal';
import NewTagModal from './NewTagModal';

function TaskTag({ tags, addExistingTag, addNewTag }) {

  const [dropDownOpen, setDropDownOpen] = useState(false);
  const [isButtonActive, setButtonActive] = useState(false);

  const [isModalOpen, setIsModalOpen] = useState(false);
  const [customTagName, setCustomTagName] = useState("");
  const inputRef = useRef(null);

  useEffect(() => {
    if (isModalOpen && inputRef.current) {
      setTimeout(() => inputRef.current?.focus(), 10);
      inputRef.current.focus();
    }
  }, [isModalOpen]);
  const handleModalOpen = () => {
    setIsModalOpen(true);

  }
  const handleModalClose = () => {
    setIsModalOpen(false);
  }
  const handleCreateSubmit = () => {
    addNewTag(customTagName);
    setIsModalOpen(false);
  }
  const handleMouseEnter = () => {
    setDropDownOpen(true);
    setButtonActive(true);
  }
  const handleMouseLeave = () => {
    setDropDownOpen(false);
    setButtonActive(false);
  }
  useEffect(() => { }, [tags]);

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
        <div id="dropdown" className="absolute -left-10 z-30 bg-white dark:bg-gray-800 divide-y divide-gray-100 rounded-lg shadow-sm w-fit">

          <ul className="p-0.5 text-sm text-gray-700 dark:text-gray-100 " aria-labelledby="dropdownDefaultButton">
            <li key="new-tag">
              <button
                onClick={handleModalOpen}
                className="flex w-full items-center justify-center px-2 py-1 hover:bg-gray-300 dark:hover:bg-gray-500 font-medium rounded-lg cursor-pointer">New Tag</button >
            </li>
            {tags.map(t => (
              <li key={t.id}>
                <span className="block px-2 py-1 hover:bg-gray-300 dark:hover:bg-gray-500 hover:rounded-lg cursor-pointer" onClick={() => addExistingTag(t)}>{t.name}</span>
              </li>
            ))}
          </ul>

        </div>
      }
      <ReactModal
        isOpen={isModalOpen}
        onRequestClose={handleModalClose}
        shouldFocusAfterRender={true}
        className="bg-gray-100 dark:bg-gray-800 rounded-lg shadow-lg p-6 w-3/4 max-w-md mx-auto my-auto focus:outline-none"
        overlayClassName="fixed inset-0 bg-black bg-opacity-50 flex items-start justify-center z-50"
      >
        <div className="flex-col justify-start items-center">
          <ul className="divide-y divide-gray-200">
            <li className="flex justify-center px-6 pb-4">
              <span className="font-bold text-xl">Enter new tag name:</span>
            </li>
            <li className="flex items-center px-6 py-4 mt-0">
              <input className="bg-gray-50 border focus:outline-none focus:ring-2 focus:ring-black focus:border-transparent border-gray-300 text-gray-900 text-sm rounded-lg mx-auto block w-1/2 p-2.5 text-center"
                type="text" placeholder="New tag"
                autoFocus
                ref={inputRef}
                onChange={(e) => setCustomTagName(e.target.value)} />
            </li>
            <li className="flex items-center px-6 pt-4 mt-0">
              <div className="flex flex-1 items-center justify-around">
                <button 
                onClick={handleCreateSubmit}
                className="bg-green-200 font-medium text-black rounded-lg border px-6 py-1 hover:bg-green-300">Create</button>
                <button onClick={handleModalClose}
                className="bg-red-300 font-medium text-black rounded-lg border px-6 py-1 hover:bg-red-400"
                >Back</button>
              </div>
            </li>
          </ul>


        </div>

      </ReactModal>
    </div>
  )

}

export default TaskTag