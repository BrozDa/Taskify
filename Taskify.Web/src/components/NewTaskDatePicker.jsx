import DatePicker from "react-datepicker";
import { useState } from 'react';

import "react-datepicker/dist/react-datepicker.css";

function NewTaskDatePicker({currentDate, setCurrentDate}) {
  const [isOpen, setIsOpen] = useState(false);

  let formattedDate = new Intl.DateTimeFormat('cs-CZ', {
    dateStyle: 'medium',
  }).format(currentDate);

  const handleChange = (e) => {
    setIsOpen(!isOpen);
    setCurrentDate(e);
  };
  const handleClick = (e) => {
    setIsOpen(!isOpen);
  };

  return (
    <div className="z-10">
      <button
        id="dateButton"
        className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center inline-flex items-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800 justify-center w-36"
        type="button"
        onClick={() => handleClick()}
      >
        {currentDate ? formattedDate : "Select due date"}
      </button>
      {isOpen &&
        <div id="dropdown" className="absolute z-10 bg-white divide-y divide-gray-100 rounded-lg shadow-sm w-32 dark:bg-gray-700">
          <DatePicker selected={currentDate} onChange={handleChange} inline />
        </div>
      }
    </div>
  )
}

export default NewTaskDatePicker