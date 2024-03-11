const TOP_OFFSET = 20

function onChangeFile() {
    const input = document.getElementById('file').files[0]
    const image = document.getElementById('image')

    input.value = ''

    const reader = new FileReader()
    reader.readAsDataURL(input)

    reader.onload = () => {
        image.src = reader.result
        image.style['opacity'] = '1'
    };
    reader.onerror = () => {
        alert('Не удалось загрузить файл')
    }
}

function onClickButton() {
    const input = document.getElementById('file')
    input.click()
}

function main() {
    const image = document.getElementById('image')
    const area = document.getElementById('area')
    area.addEventListener('dragend', (e) => {
        let left = e.x
        let top = e.y
        if (left < 0) 
            left = 0
        if (left > document.body.clientWidth - image.clientWidth) 
            left = document.body.clientWidth - image.clientWidth
        if (top < TOP_OFFSET)
            top = TOP_OFFSET
        if (top > document.body.clientHeight - image.clientHeight)
            top = document.body.clientHeight - image.clientHeight

        image.style.left = left + 'px'
        image.style.top = top + 'px'
    })
}

main()