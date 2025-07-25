import { Link } from 'react-router-dom';
import LogoutButton from './LogoutButton';

function SideBar({isSideBarOpen}) {

  return (
    <aside id="default-sidebar" className={`fixed top-0 left-0 z-40 w-48 h-screen transition-transform ${isSideBarOpen ? 'translate-x-0' : '-translate-x-full'
        } sm:translate-x-0`}>
    <div className="h-full px-3 py-4 mt-12  overflow-y-auto bg-gray-100">
      <ul className="space-y-2 font-medium">
          <li>
            <Link
            to={"/dashboard"}
            className="flex items-center p-2 text-gray-700 hover:bg-gray-200 hover:text-gray-900 rounded-lg">
                <svg xmlns="http://www.w3.org/2000/svg" className="h-6 w-6 mr-2" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2"
                        d="M12 8v4l3 3m6-3a9 9 0 11-18 0 9 9 0 0118 0z" />
                    </svg>
            Pending
            </Link>
          </li>
          <li>
            <Link
            to={"/dashboard/completed"}
            className="flex items-center p-2 text-gray-700 hover:bg-gray-200 hover:text-gray-900 rounded-lg">
                <svg xmlns="http://www.w3.org/2000/svg" className="h-6 w-6 mr-2" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2"
                        d="M12 8v4l3 3m6-3a9 9 0 11-18 0 9 9 0 0118 0z" />
                    </svg>
            Completed
            </Link>
          </li>
          <li>
            <LogoutButton/>
          </li>
      </ul>
   </div>
   </aside>
  )
}

export default SideBar