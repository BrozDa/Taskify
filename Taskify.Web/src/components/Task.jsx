import React from 'react'

function Task({task}) {

  const date = new Date(task.dueDate);
  const formattedDate = new Intl.DateTimeFormat('cs-CZ',{
    dateStyle: 'medium',
    timeStyle: 'short',
  }).format(date);

  return (
  <div className="w-96 m-4 rounded-xl bg-sky-400 p-4 shadow-lg text-white space-y-4">
    {/* Top Row: Priority, Task Name, Date */}
    <div className="flex justify-between items-center">
      <span className="px-2 py-1 bg-black/20 rounded text-sm font-semibold">
        {task.priority.name}
      </span>
      <h2 className="text-lg font-bold">{task.name}</h2>
      <span className="text-sm text-white/80">{formattedDate}</span>
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
          className="px-2 py-0.5 bg-white/20 rounded-full text-xs font-medium"
        >
          #{t.name}
        </span>
      ))}
    </div>
  </div>
);
}

export default Task