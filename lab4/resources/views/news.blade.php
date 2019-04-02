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

            <form action="{{ url('news/'.$newsElement->id) }}" method="POST">
                <!-- DAT IS JEWISH'S TRICKS -->
                <!-- https://nobuhiroharada.com/2018/05/26/laravel-ajax-419/ -->
                {{ method_field('DELETE') }} {{ csrf_field() }}
                <button type="submit">Delete</button>
            </form>
        </li>
        @endforeach
    </ul>

    <form action="{{ url('news') }}" method="POST">
        <div>
            <label for="title">Title</label>
            <input type="text" name="title">
        </div>
        <div>
            <label for="content">Content</label>
            <textarea name="content"></textarea>
        </div>
        {{ csrf_field() }}
        <button type="submit">Add</button>
    </form>
</body>

</html>