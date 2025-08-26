import pandas as pd

def merge_and_save_data(all_filtered_data: list, output_path: str):
    """
    合并处理过的数据，进行最终的列操作、统计，并保存到Excel文件。

    Args:
        all_filtered_data (list): 包含待合并的Pandas DataFrame的列表。
        output_path (str): 输出Excel文件的路径。
    """
    if not all_filtered_data:
        print('没有找到可以合并的数据')
        return

    print(f'总共找到 {len(all_filtered_data)} 个有效数据块，开始合并...')
    combined_df = pd.concat(all_filtered_data, ignore_index=True)
    
    print("正在创建'合并列'...")
    # 确保拼接的列都是字符串类型，避免混合类型的错误
    combined_df['合并列'] = combined_df['产品编号'].astype(str) + \
                           combined_df['站点'].astype(str) + \
                           combined_df['区域'].astype(str)

    print("正在整理最终的列...")
    final_df = combined_df[['辅助列-排序', '合并列', '销售确认发货']]

    # --- 统计数据 ---
    total_rows = len(final_df)
    empty_sort_rows = final_df['辅助列-排序'].isna().sum()
    sum_shipping_confirmation = final_df['销售确认发货'].sum()

    print("正在保存结果到Excel文件...")
    final_df.to_excel(output_path, index=False)
    print(f'成功！合并后的文件已保存到: {output_path}')

    # --- 打印统计信息 ---
    print("-" * 20)
    print(f"合并结果总共 {total_rows} 行")
    print(f"其中 '辅助列-排序' 为真实空值的行数: {empty_sort_rows}")
    print(f"'销售确认发货' 列的总计为: {sum_shipping_confirmation}")
    print("-" * 20)