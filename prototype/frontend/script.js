document.addEventListener('DOMContentLoaded', () => {
    const navItems = document.querySelectorAll('.nav-item');
    const dataContainer = document.getElementById('data-container');

    const createTableFromJSON = (data) => {
        if (!Array.isArray(data) || data.length === 0) {
            // If data is not an array or is empty, display it as preformatted text.
            const pre = document.createElement('pre');
            pre.textContent = JSON.stringify(data, null, 2);
            return pre;
        }

        const table = document.createElement('table');
        const thead = document.createElement('thead');
        const tbody = document.createElement('tbody');
        const headerRow = document.createElement('tr');

        // Create table headers from the keys of the first object
        const headers = Object.keys(data[0]);
        headers.forEach(headerText => {
            const th = document.createElement('th');
            th.textContent = headerText;
            headerRow.appendChild(th);
        });
        thead.appendChild(headerRow);

        // Create table rows
        data.forEach(obj => {
            const row = document.createElement('tr');
            headers.forEach(header => {
                const cell = document.createElement('td');
                let cellData = obj[header];
                // If the data is complex (e.g., an object or array), stringify it.
                if (typeof cellData === 'object' && cellData !== null) {
                    cellData = JSON.stringify(cellData, null, 2);
                    const pre = document.createElement('pre');
                    pre.textContent = cellData;
                    cell.appendChild(pre);
                } else {
                    cell.textContent = cellData;
                }
                row.appendChild(cell);
            });
            tbody.appendChild(row);
        });

        table.appendChild(thead);
        table.appendChild(tbody);
        return table;
    };

    const fetchData = async (url, target) => {
        // Remove active class from all items
        navItems.forEach(item => item.classList.remove('active'));
        // Add active class to the clicked item
        target.classList.add('active');

        dataContainer.innerHTML = '<p>Loading...</p>';
        try {
            const response = await fetch(url);
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            const data = await response.json();
            // Clear previous content
            dataContainer.innerHTML = '';
            const table = createTableFromJSON(data);
            dataContainer.appendChild(table);
        } catch (error) {
            dataContainer.innerHTML = `<p>Error fetching data: ${error.message}</p>`;
            console.error('Fetch error:', error);
        }
    };

    navItems.forEach(item => {
        item.addEventListener('click', (e) => {
            e.preventDefault();
            const apiUrl = e.target.dataset.api;
            fetchData(apiUrl, e.target);
        });
    });

    // Fetch data for the first item on page load
    if (navItems.length > 0) {
        const firstNavItem = navItems[0];
        const firstApiUrl = firstNavItem.dataset.api;
        fetchData(firstApiUrl, firstNavItem);
    }
});