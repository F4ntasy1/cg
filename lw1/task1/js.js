const SYMBOL_WIDTH = 120
const SYMBOL_HEIGHT = 150

function drawRect(context, rect) {
    context.fillRect(rect.x, rect.y, rect.width, rect.height)
    context.fill()
}

function drawEllipse(context, ellipse) {
    context.ellipse(ellipse.x, ellipse.y, ellipse.rx, ellipse.ry, 
        ellipse.angle, 0, 2 * Math.PI)
    context.fill()
}

function drawLastName(context, x, y) {
    context.fillStyle = 'red'
    drawRect(context, {x: x, y: y, width: SYMBOL_WIDTH, height: 30})
    drawRect(context, {x: x, y: y, width: 30, height: SYMBOL_HEIGHT})
    drawRect(context, {x: x + SYMBOL_WIDTH - 30, y: y, width: 30, height: SYMBOL_HEIGHT})
}

function drawFirstName(context, x, y) {
    const topOffset = 15
    const topHeight = 100

    context.fillStyle = 'blue'
    // Верхняя часть 
    drawRect(context, {x: x + topOffset, y: y, width: SYMBOL_WIDTH - topOffset * 2, height: 30})
    drawRect(context, {x: x + topOffset, y: y, width: 30, height: topHeight})
    drawRect(context, {x: x + SYMBOL_WIDTH - 30 - topOffset, y: y, width: 30, height: topHeight})

    // Нижняя часть
    drawRect(context, {x: x, y: y + topHeight, width: SYMBOL_WIDTH, height: 30})
    drawRect(context, {x: x, y: y + topHeight, width: 30, height: SYMBOL_HEIGHT - topHeight})
    drawRect(context, {x: x + SYMBOL_WIDTH - 30, y: y + topHeight, width: 30, height: SYMBOL_HEIGHT - topHeight})
}

function drawPatronymic(context, x, y) {
    context.fillStyle = 'green'
    drawRect(context, {x: x, y: y, width: 30, height: SYMBOL_HEIGHT})
    drawEllipse(context, {x: x + SYMBOL_WIDTH / 2, y: y + 75, rx: 17, ry: SYMBOL_WIDTH - 30, angle: Math.PI / 5})
    drawRect(context, {x: x + SYMBOL_WIDTH - 30, y: y, width: 30, height: SYMBOL_HEIGHT})
}

function getCanvasContext() {
    const canvas = document.getElementById('canvas')
    return canvas?.getContext('2d')
}

function drawInitials() {
    const context = getCanvasContext()
    drawLastName(context, 0, 0)
    drawFirstName(context, 150, 0)
    drawPatronymic(context, 300, 0)
}

function main() {
    drawInitials()
}

main()