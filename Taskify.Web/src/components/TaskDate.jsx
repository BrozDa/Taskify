import DatePicker from "react-datepicker";
import { useRef, useState } from 'react';
import "react-datepicker/dist/react-datepicker.css";

function TaskDate({
  currentDate,
  setCurrentDate,
  newTask = false,
  colors = "text-white bg-blue-700 hover:bg-blue-800"
}) {
  const [isOpen, setIsOpen] = useState(false);
  
  const formattedDate = new Intl.DateTimeFormat('cs-CZ', {
    dateStyle: 'medium',
  }).format(currentDate);

  const handleChange = (date) => {
    setCurrentDate(date);
    setIsOpen(false);
  };

  const handleClick = () => {
    setIsOpen(prev => !prev);
  };

  return (
    <div className="relative inline-block z-10 w-36">
      {newTask ? (
        <button
          id="dateButton"
          className={`${colors} focus:outline-none font-medium rounded-lg text-sm px-5 py-2.5 text-center inline-flex items-center justify-center w-36`}
          type="button"
          onClick={handleClick}
          
        >
          {currentDate ? formattedDate : "Select due date"}
        </button>
      ) : (
        <button
          className={`${colors} hover:bg-black/20 focus:bg-black/20 focus:outline-none font-medium rounded-lg text-sm px-5 py-2.5 text-center inline-flex items-center justify-center w-36`}
          onDoubleClick={handleClick}
        >
          {`Due: ${formattedDate}`}
        </button>
      )}

      {isOpen && (
        <div
          id="dropdown"
          className="absolute top-full -left-full transform translate-x-2/3 mt-1 z-10 bg-white divide-y divide-gray-100 rounded-lg shadow-sm w-32 dark:bg-gray-700"
        >
          <DatePicker selected={currentDate} onChange={handleChange} inline/>
        </div>
      )}
    </div>
  );
}

export default TaskDate;
