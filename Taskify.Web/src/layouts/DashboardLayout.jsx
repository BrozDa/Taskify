import { useState } from 'react'
import { Outlet} from 'react-router-dom';
import SideBar  from "../components/SideBar"
import Navbar from '../components/Navbar';
import Footer from '../components/Footer';
function DashboardLayout() {

   const [isSideBarOpen, setIsSideBarOpen] = useState(false);

  return (
   <div className="flex h-max">
      <SideBar isSideBarOpen={isSideBarOpen} />
      <div className="flex-1 flex flex-col">
        <Navbar isMenuOpen={isSideBarOpen} setIsMenuOpen={setIsSideBarOpen} />
        <main className="flex-1 overflow-auto py-4">
          <Outlet />
        </main>
        <footer>
          <Footer/>
      </footer>
      </div>
      
    </div>

  )
}

export default DashboardLayout