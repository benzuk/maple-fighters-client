use crate::db::characters;
use crate::db::characters::Pool;

use actix_web::{web, web::Path, Error, HttpResponse};

pub async fn delete(pool: web::Data<Pool>, id: Path<i32>) -> Result<HttpResponse, Error> {
    let conn = pool.get().unwrap();
    let character_id = id.into_inner();

    if characters::get_by_id(character_id, &conn).is_ok() {
        let is_deleted = web::block(move || characters::delete_by_id(character_id, &conn))
            .await
            .map_err(|_| HttpResponse::InternalServerError().finish())?;

        if is_deleted {
            Ok(HttpResponse::Ok().finish())
        } else {
            return Ok(HttpResponse::InternalServerError().json("Failed to delete character."));
        }
    } else {
        Ok(HttpResponse::NotFound().json("The character was not found."))
    }
}
