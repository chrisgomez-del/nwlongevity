// Please note: You can set up imports here but they wont be referenced. Please reference your script directly in the html file. Then add the import here. When we port over the work, our import list will be ready.
import Collapse from 'bootstrap/js/dist/collapse';
import '../styles/style.scss';

import StatCards from "./components/StatCards.js";

document.addEventListener('DOMContentLoaded', () => {
    StatCards('.card-stat');
});
