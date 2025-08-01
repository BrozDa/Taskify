import DarkModeToggle from "./DarkModeToggle"

function Navbar({ isMenuOpen, setIsMenuOpen }) {

  return (
    <nav className="fixed top-0 z-50 w-full bg-gray-300 border-b-2 border-gray-500 dark:bg-gray-800 dark:border-gray-400 px-3 py-3">
  <div className="flex items-center justify-between">
    <div className="flex items-center">
      <button
        onClick={() => setIsMenuOpen(!isMenuOpen)}
        type="button"
        className="inline-flex items-center ms-3 text-sm text-gray-500 rounded-lg sm:hidden hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-gray-200"
      >
        <svg className="w-6 h-6" fill="currentColor" viewBox="0 0 20 20">
          <path
            clipRule="evenodd"
            fillRule="evenodd"
            d="M2 4.75A.75.75 0 012.75 4h14.5a.75.75 0 010 1.5H2.75A.75.75 0 012 4.75zm0 10.5a.75.75 0 01.75-.75h7.5a.75.75 0 010 1.5h-7.5a.75.75 0 01-.75-.75zM2 10a.75.75 0 01.75-.75h14.5a.75.75 0 010 1.5H2.75A.75.75 0 012 10z"
          />
        </svg>
      </button>
      <span className="ml-5 self-center text-xl font-semibold sm:text-2xl whitespace-nowrap dark:text-blue-400">
        TASKIFY
      </span>
    </div>
    <div>
      <DarkModeToggle />
    </div>
  </div>
</nav>
  )
}

export default Navbar