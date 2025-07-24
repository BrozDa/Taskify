function CompletedTask({task}) {

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
  }).format(new Date(task.dueDate));

  return (
    <div className={`flex flex-1 flex-col max-w-xl justify-around ${colors[task.priority.color]} text-black m-4 rounded-xl min-h-48 p-4 shadow-lg  space-y-4`}>
      <div className="flex justify-between items-center mx-1 -mt-2">
        {/*Priority*/}
        <span
          className={`w-32 font-medium rounded-lg text-sm px-5 py-2.5 text-center inline-flex  items-center justify-center 
            ${priorityColors[task.priority.color]}`}
          type="button"
        >
          {task.priority.name}
        </span>
         {/*Date*/}
        <button
          className={`${colors} hover:bg-black/20 focus:bg-black/20 focus:outline-none font-medium rounded-lg text-sm px-5 py-2.5 text-center inline-flex items-center justify-center w-36`}
        >
          {`Due: ${formattedDate}`}
        </button>

      </div>
      {/*Name*/}
      <div>
        <h2 className="text-2xl font-bold">{task.name}</h2>
      </div>
      {/*Description*/}
      <div>
        <p className="text-sm whitespace-normal break-words text-center max-w-full">{task.description}</p>
      </div>

      {/* Tags */}
      <div className="flex flex-wrap gap-2 justify-center">
        {task.tags.map((t) => (
          <span
            key={t.id}
            className="px-2 py-0.5 bg-white/80 hover:bg-white/40 rounded-full text-xs font-medium "
          >
            #{t.name}
          </span>
        ))}
      </div>
    </div>
  );
}

export default CompletedTask