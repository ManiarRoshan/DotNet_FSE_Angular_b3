(function() {

    const taskInput = document.getElementById('new-task-input');
    const addTaskBtn = document.getElementById('add-task-btn');
    const taskList = document.getElementById('task-list');

    function createTaskElement(taskText) {
        const listItem = document.createElement('li');
        listItem.className = 'task-item';

        const span = document.createElement('span');
        span.className = 'task-text';
        span.textContent = taskText;

        const deleteBtn = document.createElement('button');
        deleteBtn.className = 'delete-btn';
        deleteBtn.textContent = 'Delete';

        listItem.appendChild(span);
        listItem.appendChild(deleteBtn);

        return listItem;
    }

    // Function to add a new task
    function addTask() {
        const text = taskInput.value;
        if (text !== '') {
            const newTask = createTaskElement(text);
            taskList.appendChild(newTask);
            taskInput.value = ''; // Clear input field
        }
    }

    // Event Delegation: Handle clicks on the task list
    function handleTaskActions(event) {
        const target = event.target;



        // Delete task
        if (target.classList.contains('delete-btn')) {
            const listItem = target.closest('.task-item');
            if (listItem) {
                listItem.remove();
            }
        }
    }


    addTaskBtn.addEventListener('click', addTask);
    taskInput.addEventListener('keypress', function(event) {
        if (event.key==='Enter') {
            addTask();
        }
    });
    taskList.addEventListener('click', handleTaskActions);

})();
