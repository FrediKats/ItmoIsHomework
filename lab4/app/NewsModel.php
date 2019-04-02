<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class NewsModel extends Model
{
    protected $table = 'news';
    protected $connection = 'mysql';
    protected $fillable = ['id', 'title', 'content'];
    public $timestamps = false;
}
