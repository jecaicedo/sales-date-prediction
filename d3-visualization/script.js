const BAR_COLORS = [
    '#3498db',
    '#e74c3c',
    '#2ecc71',
    '#f39c12',
    '#9b59b6'
];

let chartData = [];
let svg = null;

function validateInputData(inputValue) {
    const errorMessage = document.getElementById('errorMessage');
    const dataInput = document.getElementById('dataInput');
    
    errorMessage.classList.remove('show');
    dataInput.classList.remove('error');
    
    if (!inputValue.trim()) {
        showError('Por favor, ingresa al menos un número.');
        return false;
    }
    
    const numbers = inputValue.split(',').map(num => num.trim());
    
    const invalidNumbers = numbers.filter(num => {
        const parsed = parseFloat(num);
        return isNaN(parsed) || !Number.isInteger(parsed) || parsed < 0;
    });
    
    if (invalidNumbers.length > 0) {
        showError(`Los siguientes valores no son números enteros válidos: ${invalidNumbers.join(', ')}`);
        return false;
    }
    
    const validNumbers = numbers.map(num => parseInt(num, 10));
    
    if (validNumbers.some(num => num < 0)) {
        showError('No se permiten números negativos.');
        return false;
    }
    
    if (validNumbers.length === 0) {
        showError('Debe ingresar al menos un número.');
        return false;
    }
    
    return validNumbers;
}

function showError(message) {
    const errorMessage = document.getElementById('errorMessage');
    const dataInput = document.getElementById('dataInput');
    
    errorMessage.textContent = message;
    errorMessage.classList.add('show');
    dataInput.classList.add('error');
}

function getBarColor(index, totalBars) {
    if (totalBars <= BAR_COLORS.length) {
        return BAR_COLORS[index % BAR_COLORS.length];
    }
    
    const colorIndex = index % BAR_COLORS.length;
    const nextColorIndex = (index + 1) % BAR_COLORS.length;
    
    if (colorIndex === nextColorIndex && totalBars > BAR_COLORS.length) {
        return BAR_COLORS[(colorIndex + 1) % BAR_COLORS.length];
    }
    
    return BAR_COLORS[colorIndex];
}

function createBarChart(data) {
    const chartContainer = document.getElementById('chart');
    const containerWidth = chartContainer.clientWidth;
    const containerHeight = chartContainer.clientHeight;
    
    chartContainer.innerHTML = '';
    
    const margin = { top: 20, right: 30, bottom: 40, left: 80 };
    const width = containerWidth - margin.left - margin.right;
    const height = containerHeight - margin.top - margin.bottom;
    
    svg = d3.select('#chart')
        .append('svg')
        .attr('width', containerWidth)
        .attr('height', containerHeight);
    
    const g = svg.append('g')
        .attr('transform', `translate(${margin.left},${margin.top})`);
    
    const xScale = d3.scaleLinear()
        .domain([0, d3.max(data)])
        .range([0, width]);
    
    const yScale = d3.scaleBand()
        .domain(d3.range(data.length))
        .range([0, height])
        .padding(0.1);
    
    g.selectAll('.bar')
        .data(data)
        .enter()
        .append('rect')
        .attr('class', 'bar')
        .attr('x', 0)
        .attr('y', (d, i) => yScale(i))
        .attr('width', d => xScale(d))
        .attr('height', yScale.bandwidth())
        .attr('fill', (d, i) => getBarColor(i, data.length))
        .attr('rx', 4)
        .attr('ry', 4);
    
    g.selectAll('.bar-label')
        .data(data)
        .enter()
        .append('text')
        .attr('class', 'bar-label')
        .attr('x', d => Math.max(xScale(d) / 2, 10))
        .attr('y', (d, i) => yScale(i) + yScale.bandwidth() / 2)
        .text(d => d)
        .style('font-size', '14px')
        .style('font-weight', 'bold')
        .style('fill', 'white')
        .style('text-anchor', 'middle');
    
    g.append('g')
        .attr('transform', `translate(0,${height})`)
        .call(d3.axisBottom(xScale))
        .style('font-size', '12px');
    
    g.append('g')
        .call(d3.axisLeft(yScale))
        .selectAll('text')
        .text((d, i) => `Barra ${i + 1}`)
        .style('font-size', '12px');
}

function updateChart() {
    const inputValue = document.getElementById('dataInput').value;
    const validatedData = validateInputData(inputValue);
    
    if (validatedData) {
        chartData = validatedData;
        createBarChart(chartData);
    }
}

function handleKeyPress(event) {
    if (event.key === 'Enter') {
        updateChart();
    }
}

function initializeApp() {
    document.getElementById('updateBtn').addEventListener('click', updateChart);
    document.getElementById('dataInput').addEventListener('keypress', handleKeyPress);
    
    const initialData = validateInputData('4,8,15,16');
    if (initialData) {
        chartData = initialData;
        createBarChart(chartData);
    }
    
    window.addEventListener('resize', () => {
        if (chartData.length > 0) {
            createBarChart(chartData);
        }
    });
}

document.addEventListener('DOMContentLoaded', initializeApp);

function getChartData() {
    return chartData;
}

function getBarColors() {
    return BAR_COLORS;
}
