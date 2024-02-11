const STATION_COLOR = '#017598'
const STATION_WIDTH = 300
const STATION_HEIGHT = 500

function getCanvasContext() {
    const canvas = document.getElementById('canvas')
    return canvas?.getContext('2d')
}

function drawRect(context, rect, isStroke) {
    context.beginPath();
    context.roundRect(rect.x, rect.y, rect.width, rect.height, rect.radius ?? 0)
    if (isStroke) {
        context.stroke()
    } else {
        context.fill()
    }
}

function drawEllipse(context, ellipse, isStroke) {
    context.beginPath();
    context.ellipse(ellipse.x, ellipse.y, ellipse.rx, ellipse.ry, 
        ellipse.angle ?? 0, 0, 2 * Math.PI)
    if (isStroke) {
        context.stroke()
    } else {
        context.fill()
    }
}

function drawTriangle(context, triangle, isStroke) {
    context.beginPath()
    context.moveTo(triangle.p1.x, triangle.p1.y)
    context.lineTo(triangle.p2.x, triangle.p2.y)
    context.lineTo(triangle.p3.x, triangle.p3.y)
    context.closePath()
    if (isStroke) {
        context.stroke()
    } else {
        context.fill()
    }
}

function drawStation(context, x, y) {
    // 1. Основание
    context.fillStyle = STATION_COLOR
    drawRect(context, {x: x, y: y + STATION_HEIGHT, 
        width: STATION_WIDTH, height: 25, radius: 10})
    // Основной прямоугольник
    drawRect(context, {x: x + STATION_WIDTH / 4, y: y, width: STATION_WIDTH / 2, 
        height: STATION_HEIGHT - 3, radius: 60})
    drawRect(context, {x: x + STATION_WIDTH / 4, y: y + STATION_HEIGHT / 2, 
        width: STATION_WIDTH / 2, height: STATION_HEIGHT / 2 - 3, radius: 5})

    // 2. Верхняя панель
    context.fillStyle = '#fff'
    drawRect(context, {x: x + STATION_WIDTH / 3, y: y + 70, width: STATION_WIDTH / 3, 
        height: 40, radius: 30})

    // 3. Значок
    context.strokeStyle = '#fff'
    context.lineWidth = 3
    const radius = STATION_WIDTH / 6
    drawEllipse(context, {x: x + STATION_WIDTH / 2, y: y + STATION_HEIGHT / 2, 
        rx: radius, ry: radius}, true)
    drawTriangle(context, {
        p1: {x: x + STATION_WIDTH / 2 + 5, y: y + STATION_HEIGHT / 2},
        p2: {x: x + STATION_WIDTH / 2 - radius / 2, y: y + STATION_HEIGHT / 2},
        p3: {x: x + STATION_WIDTH / 2 + radius / 3, y: y + STATION_HEIGHT / 2 - radius / 1.3}
    })
    drawTriangle(context, {
        p1: {x: x + STATION_WIDTH / 2 - 5, y: y + STATION_HEIGHT / 2},
        p2: {x: x + STATION_WIDTH / 2 + radius / 2, y: y + STATION_HEIGHT / 2},
        p3: {x: x + STATION_WIDTH / 2 - radius / 3, y: y + STATION_HEIGHT / 2 + radius / 1.3}
    })
}

function main() {
    const context = getCanvasContext();
    drawStation(context, 100, 100)
}

main()