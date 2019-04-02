<?php

namespace App\Http\Controllers;

use App\Http\Controllers\Controller;
use App\Models\NewsModel;

class NewsController extends Controller
{
    public function index()
    {
        $newsElement = new NewsModel;
        $newsElement->id = 1;
        $newsElement->title = 'title';
        $newsElement->content = 'content';

        return view('news', ['newsList' => [$newsElement]]);
    }

    public function store()
    {
        return view('/welcome');
    }

    public function destroy($id)
    {
        return view('/welcome');
    }
}