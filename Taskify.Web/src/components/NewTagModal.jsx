import React from 'react'

function NewTagModal({isOpen, handleClose, newTag}) {

  if(!isOpen) return;

  return (
    <div className='z-50'>NewTagModal</div>
  )
}

export default NewTagModal