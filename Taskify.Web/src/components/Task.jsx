import React from 'react'
import clsx from 'clsx';
import ButtonTaskDelete from './ButtonTaskDelete';

function Task({ task, handleDelete }) {

  const date = new Date(task.dueDate);
  const colors = {
    red: "bg-red-200 text-red-800",
    orange: "bg-orange-200 text-orange-800",
    yellow: "bg-yellow-200 text-yellow-800",
    green: "bg-green-200 text-green-800",
  }
  const priorityColors = {
    red: "bg-red-400 hover:bg-red-400/50",
    orange: "bg-orange-400 hover:bg-orange-400/50",
    yellow: "bg-yellow-400 hover:bg-yellow-400/50",
    green: "bg-green-400 hover:bg-green-400/50",
  }

  const formattedDate = new Intl.DateTimeFormat('cs-CZ', {
    dateStyle: 'medium',
  }).format(date);


  return (
    <div className={`relative flex flex-col w-96 justify-around ${colors[task.priority.backgroundClass]} rounded-xl min-h-48 shadow-lg p-4 m-4`}>
      <div className=" absolute top-2 right-2 z-20" >
        <ButtonTaskDelete task={task} handleDelete={handleDelete} />
      </div>
      <div className="flex justify-between items-center mx-1 -mt-2">
        <span className={`px-2 py-2 ${priorityColors[task.priority.backgroundClass]} rounded text-sm font-semibold`}>
          {task.priority.name}
        </span>
        <span className={`text-sm font-semibold ${colors[task.priority.backgroundClass]}`}>{`Due: ${formattedDate}`}</span>

      </div>
      <div>
        <h2 className="text-2xl font-bold">{task.name}</h2>

      </div>
      {/* Description */}
      <div>
        <p className="text-sm">{task.description}</p>
      </div>

      {/* Tags */}
      <div className="flex flex-wrap gap-2">
        {task.tags.map((t) => (
          <span
            key={t.id}
            className="px-2 py-0.5 bg-white/80 hover:bg-white/40 rounded-full text-xs font-medium"
          >
            #{t.name}
          </span>
        ))}
      </div>
    </div>
  );
}

export default Task