import React, { useState } from 'react'
import NewTaskPriority from './NewTaskPriority';
import NewTaskDatePicker from './NewTaskDatePicker';
function NewTask({priorities, tags}) {

  const [newPriority, setNewPriority] = useState(null);
  const [newTaskDueDate, setNewTaskDueDate] = useState(Date.now());
  const [newTaskName, setNewTaskName] = useState("New Task Name");
  const [newTaskDescription, setNewTaskDescription] = useState("New Task Description");
  const [newTaskTags, setNewTaskTags] = useState([]);

  return (
  <div className={`flex-col w-96 justify-around bg-blue-400 text-black m-4 rounded-xl min-h-48 p-4 shadow-lg  space-y-4`}>
    <div className="flex justify-between items-center">
      <span className={`px-2 py-2 bg-blue-400 text-black rounded text-sm font-semibold`}>
        <NewTaskPriority priorities={priorities} currentPriority = {newPriority} setNewPriority={setNewPriority}/>
      </span>
      <span className={`text-sm font-semibold bg-blue-400 text-black`}>
        <NewTaskDatePicker currentDate={newTaskDueDate} setCurrentDate={setNewTaskDueDate}/>
      </span>
    </div>
    <div>
      <h2 className="text-2xl font-bold">TASK NAME</h2>
      
    </div>
    {/* Description */}
    <div>
      <p className="text-sm">TASK DESCRIPTION</p>
    </div>

    {/* Tags */}
    <div className="flex flex-wrap gap-2">
      TAGS
    </div>
  </div>
);
}

export default NewTask