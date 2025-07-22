import { BrowserRouter as Router, Routes, Route, Link } from "react-router-dom";
import Viewer from "./Pages/Viewer";
import Admin from "./Pages/Admin";
import './App.css';


function App() {
    return (
        <Router>
            <div>
                <Routes>
                    <Route path="/viewer" element={<Viewer /> } />
                    <Route path="/admin" element={<Admin /> } />
                </Routes>
            </div>
        </Router>
    );
}

export default App;