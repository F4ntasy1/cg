const COLOR = '#017598'
const STATION_WIDTH = 300
const STATION_HEIGHT = 500

const SOCKET_WIDTH = 600
const SOCKET_HEIGHT = 400

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

function drawStationLayout(context, x, y) {
    context.fillStyle = COLOR
    // Основание
    drawRect(context, {x: x, y: y + STATION_HEIGHT, 
        width: STATION_WIDTH, height: 25, radius: 10})
    // Основной прямоугольник
    drawRect(context, {x: x + STATION_WIDTH / 4, y: y, width: STATION_WIDTH / 2, 
        height: STATION_HEIGHT - 3, radius: 60})
    drawRect(context, {x: x + STATION_WIDTH / 4, y: y + STATION_HEIGHT / 2, 
        width: STATION_WIDTH / 2, height: STATION_HEIGHT / 2 - 3, radius: 5})
    // Верхняя панель
    context.fillStyle = '#fff'
    drawRect(context, {x: x + STATION_WIDTH / 3, y: y + 70, width: STATION_WIDTH / 3, 
        height: 40, radius: 30})
}

function drawStationIcon(context, x, y) {
    const radius = STATION_WIDTH / 6

    context.strokeStyle = '#fff'
    context.lineWidth = 3
    // 1. Обводка
    drawEllipse(context, {x: x + STATION_WIDTH / 2, y: y + STATION_HEIGHT / 2, 
        rx: radius, ry: radius}, true)
    // 2. Значок
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

function drawStation(context, x, y) {
    drawStationLayout(context, x, y)
    drawStationIcon(context, x, y)
}

function drawCable(context, x, y) {
    drawRect(context, {x: x, y: y + SOCKET_HEIGHT, 
        width: SOCKET_WIDTH / 7, height: 20, radius: 8})
    drawEllipse(context, {x: x + SOCKET_WIDTH / 7 + 19, y: y + SOCKET_HEIGHT + 55, 
        rx: 10, ry: 60, angle: -Math.PI / 7})
    drawEllipse(context, {x: x + SOCKET_WIDTH / 7 + 78, y: y + SOCKET_HEIGHT + 37, 
        rx: 10, ry: 80, angle: Math.PI / 7})
    drawEllipse(context, {x: x + SOCKET_WIDTH / 7 + 130, y: y + SOCKET_HEIGHT, 
        rx: 10, ry: 40, angle: -Math.PI / 7})
    drawRect(context, {x: x + SOCKET_WIDTH / 7 + 140, y: y + SOCKET_HEIGHT + 17, 
        width: SOCKET_WIDTH / 9, height: 20, radius: 8})
    drawRect(context, {x: x + SOCKET_WIDTH / 2.2, y: y + SOCKET_HEIGHT / 1.7, 
        width: 20, height: SOCKET_HEIGHT / 2, radius: 8})
}

function drawPlugPart(context, x, y) {
    drawRect(context, {x: x, y: y, width: 20, height: 77, radius: 15})
}

function drawPlug(context, x, y) {
    const plugWidth = SOCKET_WIDTH / 3.5
    
    const plugCenterX = x + SOCKET_WIDTH / 2.2 + 10
    const plugStartX = plugCenterX - SOCKET_WIDTH / 7
    const plugEndX = plugStartX + plugWidth

    const plugBottomY = y + SOCKET_HEIGHT / 1.7 + 20
    const plugTopY = plugBottomY - SOCKET_HEIGHT / 3.4

    // 1. Вилка
    drawTriangle(context, {
        p1: {x: plugCenterX, y: plugBottomY},
        p2: {x: plugCenterX - SOCKET_WIDTH / 8, y: plugBottomY - SOCKET_HEIGHT / 5},
        p3: {x: plugCenterX + SOCKET_WIDTH / 8, y: plugBottomY - SOCKET_HEIGHT / 5}
    })
    drawRect(context, {x: plugStartX, y: plugTopY, 
        width: plugWidth, height: 35, radius: 10})
    // 2. Части вилки
    drawPlugPart(context, plugStartX + plugWidth / 5, plugTopY - 80)
    drawPlugPart(context, plugEndX - plugWidth / 5 - 20, plugTopY - 80)
}

function drawSocket(context, x, y) {
    context.fillStyle = COLOR
    drawCable(context, x, y)
    drawPlug(context, x, y)
}

function main() {
    const context = getCanvasContext();
    drawStation(context, 100, 100)
    drawSocket(context, 200 + STATION_WIDTH / 2, 100)
}

main()