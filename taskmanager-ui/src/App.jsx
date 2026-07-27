import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';
import ProjectsList from './components/ProjectsList';
import ProjectDetails from './components/ProjectDetails';
import './index.css';

function App() {
  return (
    <BrowserRouter>
      <div className="app-container">
        <nav className="navbar">
          <h2>Task Manager</h2>
          <Link to="/">Projects</Link>
        </nav>
        <main className="main-content">
          <Routes>
            <Route path="/" element={<ProjectsList />} />
            <Route path="/project/:id" element={<ProjectDetails />} />
          </Routes>
        </main>
      </div>
    </BrowserRouter>
  );
}

export default App;
