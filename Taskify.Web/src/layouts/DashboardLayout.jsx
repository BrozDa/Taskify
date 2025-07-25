import { useState } from 'react'
import { Outlet} from 'react-router-dom';
import SideBar  from "../components/SideBar"
import Navbar from '../components/Navbar';

function DashboardLayout() {

   const [isSideBarOpen, setIsSideBarOpen] = useState(false);

  return (
   <div className="flex h-screen">
      <SideBar isSideBarOpen={isSideBarOpen} />
      <div className="flex-1 flex flex-col">
        <Navbar isMenuOpen={isSideBarOpen} setIsMenuOpen={setIsSideBarOpen} />
        <main className="flex-1 overflow-auto p-4">
          <Outlet />
        </main>
      </div>
    </div>

  )
}

export default DashboardLayout