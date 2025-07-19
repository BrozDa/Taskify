import React from 'react'

function Task({task}) {
  return (
    <div>
      <p>{task.priority}</p>
    </div>
  )
}

export default Task