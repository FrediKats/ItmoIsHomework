<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Http\Controllers\Controller;
use App\NewsModel;

class NewsController extends Controller
{
    public function index()
    {
        return view('news', ['newsList' => NewsModel::all()]);
    }

    public function store(Request $request)
    {
        NewsModel::create([
            'title' => $request->title,
            'content' => $request->content
        ]);
        return redirect('/news');
    }

    public function destroy(int $id)
    {
        NewsModel::where('id', $id)->delete();
        return redirect('/news');
    }
}