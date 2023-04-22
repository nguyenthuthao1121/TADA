namespace TADA.Dto.Order;

public class CategoryShipping
{
    public string level1 { get; set; }
}

public class Item
{
    public string name { get; set; }
    public string code { get; set; }
    public int quantity { get; set; }
    public int price { get; set; }
    public int length { get; set; }
    public int width { get; set; }
    public int height { get; set; }
    public CategoryShipping category { get; set; }
}

public class OrderShipping
{
    public int payment_type_id { get; set; }
    public string note { get; set; }
    public string from_name { get; set; }
    public string from_phone { get; set; }
    public string from_address { get; set; }
    public string from_ward_name { get; set; }
    public string from_district_name { get; set; }
    public string from_province_name { get; set; }
    public string required_note { get; set; }
    public string return_name { get; set; }
    public string return_phone { get; set; }
    public string return_address { get; set; }
    public string return_ward_name { get; set; }
    public string return_district_name { get; set; }
    public string return_province_name { get; set; }
    public string client_order_code { get; set; }
    public string to_name { get; set; }
    public string to_phone { get; set; }
    public string to_address { get; set; }
    public string to_ward_name { get; set; }
    public string to_district_name { get; set; }
    public string to_province_name { get; set; }
    public int cod_amount { get; set; }
    public string content { get; set; }
    public int weight { get; set; }
    public int length { get; set; }
    public int width { get; set; }
    public int height { get; set; }
    public int cod_failed_amount { get; set; }
    public int pick_station_id { get; set; }
    public object deliver_station_id { get; set; }
    public int insurance_value { get; set; }
    public int service_id { get; set; }
    public int service_type_id { get; set; }
    public object coupon { get; set; }
    public object pick_shift { get; set; }
    public int pickup_time { get; set; }
    public int shop_id { get; set; }
    public List<Item> items { get; set; }
}
public class Data
{
    public string order_code { get; set; }
    public string sort_code { get; set; }
    public string trans_type { get; set; }
    public string ward_encode { get; set; }
    public string district_encode { get; set; }
    public Fee fee { get; set; }
    public int total_fee { get; set; }
    public DateTime expected_delivery_time { get; set; }
}

public class Fee
{
    public int main_service { get; set; }
    public int insurance { get; set; }
    public int cod_fee { get; set; }
    public int station_do { get; set; }
    public int station_pu { get; set; }
    public int @return { get; set; }
    public int r2s { get; set; }
    public int coupon { get; set; }
    public int document_return { get; set; }
    public int double_check { get; set; }
    public int pick_remote_areas_fee { get; set; }
    public int deliver_remote_areas_fee { get; set; }
    public int cod_failed_fee { get; set; }
}

public class ResponseOrder
{
    public int code { get; set; }
    public string code_message_value { get; set; }
    public Data data { get; set; }
    public string message { get; set; }
    public string message_display { get; set; }
}
