<?php

namespace App\Http\Controllers;

use App\Http\Controllers\Controller;

class NewsController extends Controller
{
    public function index()
    {
        return view('news', ['newsList' => []]);
    }

    public function store($id)
    {
        return view('news');
    }

    public function destroy($id)
    {
        return view('news');
    }
}