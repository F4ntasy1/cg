function onChangeFile() {
    const input = document.getElementById("file").files[0]
    const image = document.getElementById('image')

    input.value = ""

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
    const input = document.getElementById("file")
    input.click()
}

function main() {
    
}

main()