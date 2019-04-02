<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>News board</title>
</head>

<body>
    <h2>News portal</h2>
    <ul>
        @foreach ($newsList as $newsElement)
        <li>
            <p>{{ $newsElement->id }} | {{ $newsElement->title }}</p>
            <p>{{ $newsElement->content }}</p>

            <form>
                <button type="submit">Delete</button>
            </form>
        </li>
        @endforeach
    </ul>

    <form>
        <div>
            <label for="title">Title</label>
            <input type="text" name="title">
        </div>
        <div>
            <label for="content">Content</label>
            <textarea name="content"></textarea>
        </div>
        <button type="submit">Add</button>
    </form>
</body>

</html>